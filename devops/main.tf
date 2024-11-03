provider "azurerm" {
  features {}
}

terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=4.7.0"
    }
  }
  backend "azurerm" {
    resource_group_name  = "rg_ds_storages"
    storage_account_name = "authapistates"
    container_name       = "states"
    key                  = "terraform.tfstate"
  }
}

resource "azurerm_resource_group" "rg" {
  name     = "rg_ds_project_authapi"
  location = "East US 2"
}

resource "azurerm_service_plan" "asp" {
  name                = "asp_ds_authapi"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  os_type             = "Linux"
  sku_name            = "B1"
}

resource "azurerm_linux_web_app" "as" {
  name                = "asdsauthapi"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_service_plan.asp.location
  service_plan_id     = azurerm_service_plan.asp.id

  site_config {}
}
