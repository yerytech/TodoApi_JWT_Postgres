# 📝 Todo API con Autenticación (C# + PostgreSQL + JWT)

Esta es una API REST construida con **ASP.NET Core**, que permite registrar y autenticar usuarios, y manejar una lista de tareas (_Todo tasks_) asociadas a cada uno. Incluye autenticación segura con **JWT** y uso de **Entity Framework Core** con base de datos **PostgreSQL**.

---

## 🔧 Tecnologías utilizadas

- ASP.NET Core 9
- Entity Framework Core
- PostgreSQL
- JWT (JSON Web Token)
- BCrypt (hash de contraseñas)
- Swagger (documentación de la API)
- Visual Studio Code

---

## 📂 Estructura del proyecto

```
TodoApi/
├── Controllers/
│   ├── AuthController.cs
│   └── TasksController.cs
├── Data/
│   └── AppDbContext.cs
├── DTOs/
│   ├── UserRegisterDto.cs
│   ├── UserLoginDto.cs
│   └── TaskDto.cs
├── Models/
│   ├── User.cs
│   └── TodoTask.cs
├── Services/
│   └── TokenService.cs
├── Program.cs
├── appsettings.json
└── TodoApi.csproj
```

---

## 🧪 Endpoints principales

### 🔐 Auth

- `POST /api/auth/register` – Registro de usuario
- `POST /api/auth/login` – Inicio de sesión y generación de JWT

### 📋 Tasks (Requiere JWT en el header)

- `GET /api/tasks` – Listar tareas del usuario autenticado
- `POST /api/tasks` – Crear tarea
- `PUT /api/tasks/{id}` – Editar tarea
- `DELETE /api/tasks/{id}` – Eliminar tarea

---

## ⚙️ Configuración

1. Crea un archivo `appsettings.json` en la raíz del proyecto con tu clave JWT y cadena de conexión:

```json
{
  "Jwt": {
    "Key": "ElultramegaPassword123!@"
  },
  "ConnectionStrings": {
    "DefaultConnections": "Host=localhost;Port=5432;Database=tododb;Username=pc;Password=yerytech"
  }
}
```

2. Ejecuta las migraciones:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

3. Ejecuta el proyecto:

```bash
dotnet run
```

La API estará disponible en: [http://localhost:5245](http://localhost:5245)

Swagger UI: [http://localhost:5245/swagger](http://localhost:5245/swagger)

---

## 🦪 Pruebas con Postman

1. Regístrate en `/api/auth/register`
2. Loguéate en `/api/auth/login` y copia el token
3. Usa el token en Postman (Authorization → Bearer Token)
4. Accede a los endpoints de `/api/tasks`

---

## 🛡️ Seguridad

- Contraseñas hasheadas con `BCrypt`
- JWT firmado con clave secreta segura
- Relación uno a muchos entre Usuario y Tareas
- Eliminación en cascada (`OnDelete.Cascade`)

---

## 📌 Autor

Desarrollado por **YeryTech**
🚀 Proyecto de práctica de backend con C# y .NET

---

## 📄 Licencia

MIT
