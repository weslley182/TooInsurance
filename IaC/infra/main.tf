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

# resource "aws_instance" "app_server" {
#   #Ubuntu Server 20.04 LTS (HVM), SSD Volume Type 64bits
#   ami           = "ami-0c65adc9a5c1b5d7c"
#   instance_type = var.instance-type
#   key_name = var.key_name
#   security_groups = [var.security_group_name]

#   tags = {
#     Name = "TooInsuranceAPI"
#   }
# }

resource "aws_launch_template" "machine" {
  description = "TooInsuranceAPIDesc"  
  name = "TooInsuranceAPIName"
  image_id = "ami-0c65adc9a5c1b5d7c"
  instance_type = var.instance-type
  key_name = var.key_name
  security_group_names = [var.security_group_name]

  tags = {
    name = "TooInsuranceAPItag"
  }  
  //user_data = var.PROD ? filebase64("ansible.sh") : ""
  user_data = filebase64("ansible.sh")
}

resource "aws_key_pair" "SSH_key" {
  key_name = var.key_name
  public_key = file("${var.key_name}.pub")
}

# output "public_IP" {
#   value = aws_instance.app_server.public_ip
# }

resource "aws_autoscaling_group" "scaling_group" {
  availability_zones = [ "${var.aws_region}a", "${var.aws_region}b" ]
  name = var.scaling_group_name
  max_size = var.max
  min_size = var.min
  launch_template {
    id = aws_launch_template.machine.id
    version = "$Latest"
  }
  target_group_arns = var.PROD ? [ aws_lb_target_group.target_group_lb[0].arn ] : []
}

resource "aws_default_subnet" "subnet_1" {
  availability_zone = "${var.aws_region}a"
}

resource "aws_default_subnet" "subnet_2" {
  availability_zone = "${var.aws_region}b"
}

resource "aws_lb" "load_balancer" {
  internal = false
  subnets = [ aws_default_subnet.subnet_1.id, aws_default_subnet.subnet_2.id ]
  count = var.PROD ? 1 : 0
}

resource "aws_lb_target_group" "target_group_lb" {
  name = "targetmachine"
  port = "5095"
  protocol = "HTTP"
  vpc_id = aws_default_vpc.default_vpc.id
  count = var.PROD ? 1 : 0
}

resource "aws_default_vpc" "default_vpc" {
  
}

resource "aws_lb_listener" "lb_in" {
  load_balancer_arn = aws_lb.load_balancer[0].arn
  port = "5095"
  protocol = "HTTP"
  default_action {
    type = "forward"
    target_group_arn = aws_lb_target_group.target_group_lb[0].arn
  }
  count = var.PROD ? 1 : 0
}

resource "aws_autoscaling_policy" "scale_policy" {
  name = "terraform_scale"
  autoscaling_group_name = var.scaling_group_name
  policy_type = "TargetTrackingScaling"
  target_tracking_configuration {
    predefined_metric_specification {
      predefined_metric_type = "ASGAverageCPUUtilization"
    }
    target_value = 50.0
  }
  depends_on = [aws_autoscaling_group.scaling_group]
  count = var.PROD ? 1 : 0
}