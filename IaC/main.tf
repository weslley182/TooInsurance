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
  region  = "us-west-2"
}

resource "aws_instance" "app_server" {
  #Ubuntu Server 20.04 LTS (HVM), SSD Volume Type 64bits
  ami           = "ami-0c65adc9a5c1b5d7c"
  instance_type = "t2.micro"
  key_name = "tookeys"

  tags = {
    Name = "TooInsuranceAPI"
  }
}