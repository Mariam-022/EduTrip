# EduTrip
**Your Gateway to Global Education**

[![Status](https://img.shields.io/badge/Status-In%20Development-yellow)](https://github.com/Mariam-022/EduTrip)
[![Platform](https://img.shields.io/badge/Platform-Web-blue)](https://github.com)

---

##  Overview

EduTrip is a comprehensive educational platform connecting students with **10,000+ fully funded scholarships** and expert mentors worldwide. The platform combines structured learning paths, scientific research guidance, and personalized mentorship within a secure, coin-based economy.

**What We Offer:**
-  **500+** Curated Courses & Learning Paths
-  **1,000+** Expert Mentors & Industry Professionals  
-  **4-Stage** Scientific Research Roadmap
-  **Virtual Coin Economy** for seamless transactions
-  **Fully Funded** Scholarship Database

---

##  Key Features

### Core Functionalities
- **User Authentication** - Email/password + Google OAuth 2.0
- **Personalized Dashboard** - Track courses, sessions, progress
- **Courses & Certifications** - Video-based learning with quizzes
- **Research Road** - 4-stage structured research pathway
- **Mentor Booking** - Real-time appointment scheduling
- **Video Sessions** - Integrated Zoom/Google Meet
- **Coin-Based Payments** - Virtual economy for seamless transactions
- **Feedback System** - 5-star ratings and reviews
- **Notifications** - Real-time alerts and reminders

---

##  Tech Stack

| Layer | Technology |
|-------|-----------|
| **Frontend** | HTML5, CSS3, Bootstrap 5, JavaScript |
| **Backend** | C#, .NET Core, ASP.NET MVC |
| **API** | .NET Core Web API (RESTful) |
| **Database** | SQL Server + Entity Framework Core |
| **Payments** | Stripe/PayPal (PCI-DSS compliant) |
| **Email** | SendGrid/AWS SES |
| **Video** | Zoom/Google Meet integration |
| **Auth** | JWT + Google OAuth 2.0 |

---

##  Quick Start

### Prerequisites
- Visual Studio 2022+
- .NET Core SDK 6.0+
- SQL Server 2019+
- Git

### Setup

```bash
# Clone repository
git clone https://github.com/Mariam-022/EduTrip.git
cd EduTrip

# Restore dependencies
dotnet restore

# Update database
dotnet ef database update

# Run application
dotnet run
```

Access at: `https://localhost:7001`

---

##  API Endpoints

### Authentication
```
POST   /api/auth/register
POST   /api/auth/login
POST   /api/auth/verify-email
```

### Courses
```
GET    /api/courses
GET    /api/courses/{id}
POST   /api/courses/{id}/enroll
```

### Mentor & Sessions
```
GET    /api/mentors
POST   /api/sessions/book
GET    /api/sessions/{id}/feedback
```

### Coin System
```
GET    /api/wallet
POST   /api/coins/purchase
POST   /api/payments/process
```

[Full API Documentation →](docs/API_ENDPOINTS.md)

---

##  User Roles

- **Students** - Enroll courses, book mentors, track progress
- **Mentors** - Manage sessions, provide guidance, view feedback
- **Admins** - Content management, moderation, analytics

---

##  Business Model

### Revenue Streams
1. **Packages** - Bundled services (Basic/Premium/Elite)
2. **Freemium** - Free courses + premium mentorship
3. **À la Carte** - Individual services (CV review, interviews, etc.)

---

## 📁 Project Structure

```
EduTrip/
├── Controllers/          # MVC & API Controllers
├── Models/              # Entity models
├── Services/            # Business logic layer
├── Data/                # EF Core DbContext
├── Views/               # ASP.NET MVC views
├── wwwroot/             # Static files (CSS, JS, images)
├── Migrations/          # Database migrations
└── Tests/               # Unit & integration tests
```

---

## 🔒 Security Features

- Email & phone verification
- Password hashing & encryption
- PCI-DSS compliant payments
- JWT token authentication
- GDPR data protection
- Admin content moderation
