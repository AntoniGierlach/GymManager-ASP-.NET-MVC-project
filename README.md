#  GymManager

GymManager to webowa aplikacja do zarządzania karnetami na siłownię, stworzona w technologii **ASP.NET Core MVC** z wykorzystaniem **Entity Framework Core** oraz **ASP.NET Identity**.  
Projekt został wykonany jako aplikacja demonstracyjna w ramach zaliczenia przedmiotu z zakresu tworzenia aplikacji webowych.

---

##  Główne założenia projektu

Aplikacja umożliwia obsługę dwóch typów użytkowników:
- **Administratora** – zarządzającego danymi systemu,
- **Zwykłego użytkownika** – korzystającego z oferty karnetów.

Celem projektu jest demonstracja:
- architektury MVC,
- pracy z bazą danych i relacjami między encjami,
- autoryzacji i uwierzytelniania użytkowników,
- implementacji pełnego mechanizmu CRUD,
- walidacji danych,
- tworzenia czytelnego i estetycznego interfejsu użytkownika.

---

##  Technologie

- ASP.NET Core MVC
- Entity Framework Core
- ASP.NET Identity
- SQLite / SQL Server
- Bootstrap 5
- HTML5 / CSS3

---

##  Role i funkcjonalności

###  Administrator
Administrator posiada dostęp do panelu administracyjnego, który umożliwia:

- **Zarządzanie karnetami (CRUD)**  
  - dodawanie, edytowanie, usuwanie karnetów,
  - określanie czasu trwania i ceny,
  - przypisywanie karnetów do oddziałów siłowni (OPEN / wybrane oddziały).

- **Zarządzanie oddziałami siłowni (CRUD)**  
  - nazwa placówki,
  - adres,
  - telefon kontaktowy,
  - email kontaktowy.

- **Podgląd zakupów karnetów**  
  - lista użytkowników,
  - zakupione karnety,
  - daty zakupu.

- **Zarządzanie użytkownikami**  
  - lista zarejestrowanych kont,
  - podstawowe dane użytkowników.

---

###  Zwykły użytkownik
Zalogowany użytkownik może:

- **Przeglądać ofertę karnetów**,
- **Zakupić karnet** (symulacja zakupu – bez rzeczywistej płatności),
- **Przeglądać swoje karnety**:
  - status (aktywny / wygasły),
  - data zakupu i data wygaśnięcia,
  - informacja, w jakich oddziałach karnet obowiązuje.

---

##  Model bazy danych

Aplikacja wykorzystuje relacyjną bazę danych. Główne encje:

- `ApplicationUser` – użytkownik systemu
- `Membership` – karnet
- `Enrollment` – zakup karnetu przez użytkownika
- `Club` – oddział siłowni
- `MembershipClub` – tabela łącząca karnety i oddziały (relacja wiele-do-wielu)

Relacje:
- Użytkownik może posiadać wiele zakupionych karnetów,
- Karnet może obowiązywać w wielu oddziałach,
- Oddział może obsługiwać wiele karnetów.

---

##  API (CRUD)

Projekt zawiera również **Web API** dla encji `Membership`:

- `GET /api/memberships` – lista karnetów
- `GET /api/memberships/{id}` – szczegóły karnetu
- `POST /api/memberships` – dodanie karnetu (Admin)
- `PUT /api/memberships/{id}` – edycja karnetu (Admin)
- `DELETE /api/memberships/{id}` – usunięcie karnetu (Admin)

API obsługuje również informacje o oddziałach, w których karnet obowiązuje.

---

##  Instalacja i uruchomienie
1. Sklonuj repozytorium
`
git clone https://github.com/AntoniGierlach/GymManager-ASP-.NET-MVC-project#
`

2. Instalacja narzędzia EF Core CLI (jeśli nie jest zainstalowane)
`
dotnet tool install --global dotnet-ef
`

3. Aplikacja korzysta z lokalnej bazy SQLite, skonfigurowanej bezpośrednio w pliku `Program.cs`

4. Wykonaj migracje
`
dotnet ef migrations add InitialCreate
dotnet ef database update
`

5. Uruchom aplikację
`
dotnet run
`

Po uruchomieniu aplikacja będzie dostępna pod adresem: `https://localhost:xxxx`

---
## Konta testowe
| Rola | Email | Hasło |
| :---: | :---: | :---: |
| Admin | admin@gym.local | Admin123! |
| User | user@gym.local | User123! |

Konta testowe są tworzone automatycznie przy starcie aplikacji (seed danych).

---
## Autorzy
[Antoni Gierlach](https://github.com/AntoniGierlach)

Projekt wykonany w ramach projektu z zakresu tworzenia aplikacji webowych.
