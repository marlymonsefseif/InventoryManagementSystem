# Inventory Management System - Backend API

## 📦 Project Overview

This is a backend system for managing product inventory. It includes features such as product management, inventory transactions, reporting, and notifications for low-stock items. It is designed using ASP.NET Core Web API and follows clean architecture principles.

---

## 🚀 Technologies Used

- ASP.NET Core Web API
- Entity Framework Core (EF Core)
- SQL Server
- JWT Authentication

---

## 📁 Features

### 1. Product Management

- **Add Product:** Name, Description, Quantity, Price, LowStockThreshold
- **Update Product**
- **Delete Product** *(soft delete using `IsDeleted`)*
- **Get Product Details**
- **List All Products** *(list of warehouse with products and list of products in system)

### 2. Inventory Transactions

- **Add Stock**
- **Remove Stock**
- **Transfer Stock** (between warehouses)

### 3. Reporting

- **Low Stock Report:** Products below their LowStockThreshold
- **Transaction History:** By product or date range *(Filter reports by: Product category)*

---

## 🔐 Role-Based Authorization

- Admins can delete products or generate reports.

