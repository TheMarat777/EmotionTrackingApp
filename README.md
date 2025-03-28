# 😊 Emotion Tracking Website

> Semester Project developed as part of the VIA University College Software Engineering program.

This is a full-stack emotion tracking system, developed by a team of 5 students. The application leverages multiple technologies and languages across its components, including C#, Java, gRPC, PostgreSQL, and Blazor.

---

## 🧩 Project Components

### 🔹 Java DAO
- Implements the Data Access Layer in Java.
- Interacts with a PostgreSQL database.
- Hosts a gRPC server to expose database functionality to external services.

**To generate gRPC classes:**
```
mvn clean install
```

**To run Flyway migrations:**
```
mvn flyway:migrate
```

---

### 🔹 C# API
- Composed of several projects:
  - `Server/Api`: The main ASP.NET Core API.
  - `Shared/Entities`: Contains domain entity classes.
  - `Protobuf`: gRPC client used to communicate with the Java DAO.

> 🔧 Protobuf classes are generated automatically when the project is built.

---

### 🔹 Protobuf
- Contains `.proto` files shared between the Java and C# services.
- Enables gRPC communication between backend components.

---

### 🔹 Client / Frontend
- Built with **Blazor Server-Side Rendering (SSR)**.
- Allows users to interact with the system via a modern web interface.

---

## 🛠️ Technologies Used
- C#
- Java
- Blazor SSR
- gRPC
- PostgreSQL
- Flyway
- Maven
- .NET

