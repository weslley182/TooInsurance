module "aws-dev" {
    source = "../../infra"
    instance-type = "t2.micro"
    aws_region = "us-west-2"
    key_name = "IaC-Prod"
    security_group_name = "general_access_prod"
    min = 1
    max = 3
    scaling_group_name = "prod_scalling"
    PROD = true
}

# output "DEV_IP" {
#   value = module.aws-dev.public_IP
# }