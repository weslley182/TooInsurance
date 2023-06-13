variable "aws_region" {
    type = string    
}

variable "key_name" {
    type = string
}

variable "instance-type" {
    type = string
}

variable "security_group_name" {    
    type = string
}

variable "scaling_group_name" {
    type = string
}

variable "max" {
    type = number
}

variable "min" {
    type = number
}

variable "PROD" {
    type = bool
}