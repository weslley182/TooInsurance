terraform init
terraform plan
terraform apply

#in ssh
ansible-playbook playbook.yml -u ubuntu --private-key tookeys.pem -i hosts.yml

verifiy rabbimq status
sudo service rabbitmq-server status

verify ports
sudo ss -tuln | grep -E "5672|15672"
