#!/bin/bash
cd /home/ubuntu
curl https://bootstrap.pypa.io/get-pip.py -o get-pip.py
sudo python3 get-pip.py
sudo python3 -m pip install ansible
tee -a playbook.yml > /dev/null <<EOT
- hosts: localhost
  become: yes
  become_method: sudo
  tasks:

## Start Inserting project from Github

  - name: Clone GitHub repository
    become: yes
    become_user: root
    ansible.builtin.git:    
      repo: https://github.com/weslley182/TooInsurance.git
      dest: /home/ubuntu/Projects
      version: Dev
      force: yes

## Finish Inserting project from Github

## Start Rabbit install and configuration
  - name: Install RabbitMQ
    apt:
      name: rabbitmq-server
      state: latest

  - name: Enable RabbitMQ Management Plugin
    shell: rabbitmq-plugins enable rabbitmq_management

  - name: Create RabbitMQ configuration directory
    become: yes
    become_user: root
    file:
      path: /etc/rabbitmq
      state: directory

  - name: Create RabbitMQ configuration file
    become: yes
    become_user: root
    copy:
      dest: /etc/rabbitmq/rabbitmq.conf
      content: |
        loopback_users = none

  - name: Restart RabbitMQ service
    become: yes
    become_user: root
    service:
      name: rabbitmq-server
      state: restarted
      enabled: true

  - name: Install curl
    apt:
      name: curl
      state: present

  - name: Wait for RabbitMQ to start
    wait_for:
      host: 127.0.0.1
      port: 15672
      delay: 10
      timeout: 60

  - name: Retrieve RabbitMQ Configuration Page
    command: curl -s http://localhost:15672
    register: config_page

  - name: Display RabbitMQ Configuration Page
    debug:
      var: config_page.stdout

## Finish Rabbit install and configuration

## Start DotNet install and config
  - name: Download dotnet 6
    get_url:
      url: "https://download.visualstudio.microsoft.com/download/pr/dd7d2255-c9c1-4c6f-b8ad-6e853d6bb574/c8e1b5f47bf17b317a84487491915178/dotnet-sdk-6.0.408-linux-x64.tar.gz"
      dest: "/tmp/dotnet-sdk-6.0.408-linux-x64.tar.gz"

  - name: Create .NET SDK directory
    become: yes
    become_user: root
    file:
      path: /opt/dotnet
      state: directory

  - name: Extract .NET 6 SDK
    command: tar -xf "/tmp/dotnet-sdk-6.0.408-linux-x64.tar.gz" -C "/opt/dotnet"

  - name: Set .NET 6 SDK path
    lineinfile:
      path: "/etc/profile"
      line: "export PATH=\"$PATH:/opt/dotnet\""
      state: present

  - name: Check .NET 6 SDK version
    shell: /opt/dotnet/dotnet --version
    register: dotnet_version

  - name: Display .NET 6 SDK version
    debug:
      var: dotnet_version.stdout

## Finish DotNet install and config

## Start to Up the system
  - name: Change permissions of a directory
    become: true
    file:
      path: /home/ubuntu/Projects
      state: directory
      mode: '0777'
      recurse: yes

  - name: Change to API project directory
    become: yes
    become_user: root
    shell: cd /home/ubuntu/Projects/InsuranceAPI/InsuranceAPI && pwd
    args:
      chdir: /home/ubuntu/Projects/InsuranceAPI/InsuranceAPI

  - name: Verify process is running
    shell: ps aux | grep InsuranceAPI
    register: process_running
    changed_when: false
    ignore_errors: true

  - name: Kill all running process
    shell: pkill -f "InsuranceAPI|HomeWorker|CarWorker"
    ignore_errors: true
    when: process_running.stdout | length > 0

  - name: Clean and build solution
    become: yes
    become_user: root
    shell: |
      export PATH="/opt/dotnet:$PATH"
      cd /home/ubuntu/Projects/InsuranceAPI
      dotnet clean
      dotnet build InsuranceAPI.sln

  - name: Run API project
    become: yes
    become_user: root
    shell: |
      export PATH="/opt/dotnet:$PATH"
      cd /home/ubuntu/Projects/InsuranceAPI/InsuranceAPI
      nohup dotnet run >/dev/null 2>&1 &

  - name: Run CarWorker project
    become: yes
    become_user: root
    shell: |
      export PATH="/opt/dotnet:$PATH"
      cd /home/ubuntu/Projects/InsuranceAPI/CarWorker
      nohup dotnet run >/dev/null 2>&1 &

  - name: Run HomeWorker project
    become: yes
    become_user: root
    shell: |
      export PATH="/opt/dotnet:$PATH"
      cd /home/ubuntu/Projects/InsuranceAPI/HomeWorker
      nohup dotnet run >/dev/null 2>&1 &

## Finish to Up the system           
EOT
ansible-playbook playbook.yml --become