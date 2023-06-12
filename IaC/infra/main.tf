terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 4.16"
    }
  }

  required_version = ">= 1.2.0"
}

provider "aws" {
  region  = var.aws_region
}

resource "aws_instance" "app_server" {
  #Ubuntu Server 20.04 LTS (HVM), SSD Volume Type 64bits
  ami           = "ami-0c65adc9a5c1b5d7c"
  instance_type = var.instance-type
  key_name = var.key_name
  security_groups = [var.security_group_name]

  tags = {
    Name = "TooInsuranceAPI"
  }
}

resource "aws_key_pair" "SSH_key" {
  key_name = var.key_name
  public_key = file("${var.key_name}.pub")
}

output "public_IP" {
  value = aws_instance.app_server.public_ip
}