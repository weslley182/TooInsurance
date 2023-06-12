terraform init
terraform plan
terraform apply

#in ssh
ansible-playbook playbook.yml -u ubuntu --private-key tookeys.pem -i hosts.yml

verifiy rabbimq status
sudo service rabbitmq-server status

verify ports
sudo ss -tuln | grep -E "5672|15672"

rabbit(change the IP for hosts.yml)
http://52.88.216.154:15672