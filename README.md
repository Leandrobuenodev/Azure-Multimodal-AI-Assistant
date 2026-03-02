# 🤖 LeIA - AI-Powered Assistant (Azure Serverless)

**LeIA** is a scalable, enterprise-grade intelligent assistant built on a **Serverless Architecture** using Microsoft Azure. This project implements real-time token streaming and automated session context management.

---

## 🏗️ System Architecture

The solution follows cloud-native patterns to ensure high availability, security, and low latency. Below is the architectural design for production environments:

![Architecture Diagram](./docs/architecture_diagram.png)
*Figure 1: Detailed architectural design and data flow.*

---

## 📸 Interface & User Experience

The frontend is a responsive chat interface that consumes the **Server-Sent Events (SSE)** stream from the backend.

### **Initial State**
When a new session starts, the assistant is ready to receive instructions with a clean, intuitive interface.

![Initial Interface](./docs/screenshot_storage.png)
*Figure 2: Welcome screen ("Hi Stranger") and sidebar session management.*

### **Technical Interaction**
The assistant provides high-level technical explanations with real-time streaming, as seen in this interaction about Azure Function models.

![Chat Interaction](./docs/screenshot_chat.png)
*Figure 3: Real-time token streaming and technical responsiveness.*

---

## 🛠️ Key Components & Technologies

### **Backend Engineering**
* **Azure Functions (.NET 8 Isolated Worker)**: High-performance ingestion layer using the latest isolated process model.
* **C# / .NET 8**: Strongly typed logic ensuring reliability and clean architecture.

### **AI & Data Management**
* **AI Foundry (GPT-4o/mini)**: Optimized connectors for generating high-quality AI responses.
* **Azure Table Storage**: Partitioned by `sessionId` for efficient and cost-effective context persistence.

---

## 🛡️ Security & Governance

Security was a priority in this architecture, following high standards for enterprise solutions:

1. **Secret Management**: All sensitive data (API Keys) are stored in **Azure Key Vault**.
2. **Managed Identity (MSI)**: Passwordless authentication between services.
3. **Data Governance**: Implemented logic for data retention and PII masking.

---

### **Author**
**Leandro Bueno** - *Full-Stack Developer*
