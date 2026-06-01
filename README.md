# 🏨 Palmier Hotel — Backend API

API REST du système de gestion hôtelière, développée avec **.NET 10** et **Clean Architecture**.

---

## 📋 Prérequis

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads) ou SQL Server Express

---

## 🚀 Installation et lancement

### 1. Cloner le dépôt

```sh
git clone https://github.com/Ibra-studio/HotelReservation.git
cd HotelReservation
```

### 2. Configurer la base de données

Dans `HotelReservation.API/appsettings.json`, modifier la connection string :

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=HotelReservationDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> Si vous utilisez une instance nommée (ex: SQLEXPRESS) :  
> `"Server=localhost\\SQLEXPRESS;Database=HotelReservationDb;Trusted_Connection=True;TrustServerCertificate=True;"`

### 3. Lancer l'API

```sh
cd HotelReservation.API
dotnet run
```

Au démarrage, l'application va automatiquement :
- ✅ Créer la base de données
- ✅ Appliquer les migrations
- ✅ Insérer les données de test si la base est vide

L'API sera disponible sur : **http://localhost:5241**  
Documentation API : **http://localhost:5241/scalar**

---

## 🔐 Comptes de test

| Rôle | Email | Mot de passe |
|------|-------|--------------|
| Administrateur | admin@hotel.com | Admin124! |
| Réceptionniste | receptionniste@hotel.com | recep123 |

---

## 🏗️ Architecture

Le projet suit les principes de la **Clean Architecture** :

```
HotelReservation.Domain          → Entités, interfaces, enums
HotelReservation.Application     → Services, DTOs, logique métier
HotelReservation.Infrastructure  → Repositories, EF Core, persistance
HotelReservation.API             → Controllers, Middleware, configuration
```

---

## 🛠️ Stack technique

- **Framework** : ASP.NET Core 10
- **ORM** : Entity Framework Core 10.0.7
- **Base de données** : SQL Server
- **Authentification** : JWT Bearer
- **Hachage** : BCrypt.Net-Next
- **Documentation** : Scalar / OpenAPI 10.0.7

---

## 📦 Dépendances principales

### HotelReservation.API
- `Microsoft.AspNetCore.Authentication.JwtBearer` 10.0.7
- `Microsoft.AspNetCore.OpenApi` 10.0.7
- `Microsoft.EntityFrameworkCore.Design` 10.0.7
- `Microsoft.EntityFrameworkCore.Tools` 10.0.7
- `Scalar.AspNetCore` 2.14.11
- `Swashbuckle.AspNetCore` 10.1.7

---

## 🚦 Points d'accès API

### Authentification
- `POST /api/users/login` - Connexion

### Utilisateurs
- `GET /api/users` - Lister tous les utilisateurs (Admin)
- `GET /api/users/{id}` - Récupérer un utilisateur (Admin)
- `POST /api/users` - Créer un utilisateur (Admin)
- `PUT /api/users/{id}` - Mettre à jour le profil
- `PUT /api/users/updatePassword/{id}` - Changer le mot de passe
- `DELETE /api/users/{id}` - Désactiver un utilisateur (Admin)
- `GET /api/users/me` - Récupérer le profil courant (Authentifié)

### Chambres
- `GET /api/chambres` - Lister toutes les chambres
- `POST /api/chambres` - Créer une chambre (Admin)
- `PUT /api/chambres/{id}` - Mettre à jour une chambre (Admin)
- `DELETE /api/chambres/{id}` - Supprimer une chambre (Admin)

### Clients
- `GET /api/clients` - Lister tous les clients
- `GET /api/clients/{id}` - Récupérer un client
- `POST /api/clients` - Créer un client
- `PUT /api/clients/{id}` - Mettre à jour un client

### Réservations
- `GET /api/reservations` - Lister les réservations
- `POST /api/reservations` - Créer une réservation
- `PUT /api/reservations/checkIn/{id}` - Check-in
- `PUT /api/reservations/checkOut/{id}` - Check-out
- `DELETE /api/reservations/{id}` - Annuler une réservation

### Factures
- `GET /api/factures` - Lister les factures
- `GET /api/factures/{id}` - Récupérer une facture

### Tarifs
- `GET /api/tarifs` - Lister les tarifs
- `POST /api/tarifs` - Créer un tarif (Admin)

### Équipements
- `GET /api/equipements` - Lister les équipements
- `POST /api/equipements` - Créer un équipement (Admin)

---

## 🔒 Sécurité

- Authentification par **JWT Bearer Token**
- Mots de passe hachés avec **BCrypt**
- Autorisation basée sur les rôles (Admin, Réceptionniste, Client)
- Middleware de gestion d'exceptions personnalisé

---

## 📝 Notes de développement

### Base de données
- Migration automatique au démarrage
- Seeding des données de test
- Utilisation de Trusted Connection (Windows Authentication)

### Middleware personnalisé
- `ExceptionMiddleware` : Gestion centralisée des exceptions
  - `KeyNotFoundException` → HTTP 404
  - `InvalidOperationException` → HTTP 400
  - `ArgumentOutOfRangeException` → HTTP 400
  - Autres exceptions → HTTP 500

---

## 📄 Licence

Ce projet est réalisé  à titre académique.

---

## 👤 Auteur

Développé par **Ibra Studio**

