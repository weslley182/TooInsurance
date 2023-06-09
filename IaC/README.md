terraform init
terraform plan
terraform apply

#in ssh
ansible-playbook playbook.yml -u ubuntu --private-key tookey.pem -i hosts.yml