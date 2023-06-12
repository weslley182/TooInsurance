module "aws-dev" {
    source = "../../infra"
    instance-type = "t2.micro"
    aws_region = "us-west-2"
    key_name = "IaC-Dev"
    security_group_name = "general_access"
}

output "DEV_IP" {
  value = module.aws-dev.public_IP
}