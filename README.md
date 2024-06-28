`Opis Projektu.`

Projekt prezentuje działanie sklepu internetowego, który umożliwia użytkownikom sklepu na składanie zamówień poprzez stronę internetową.

Strona Register - umożliwia zarejstrowanie nowego konta na stronie. Aby utworzyć nowe konto na stronie należy wpisać wymagane dane, czyli "Username"(string) oraz "Password"(string). Po kliknięciu przycisku "Register", po upływie kilku sekund zostaniemy przeniesieni na stronę "Login".

Strona Login - umożliwia zalogowanie się na wcześniej utworzone konto na stronie. Aby zalogować się należy poprawnie wypełnić pola "Username"(string) oraz "Password"(string). Na domyślne konto administratora możemy zalogować się używając nazwy "admin@example.com" oraz hasła "Admin123".

Strona Products - wyświetla ona listę produktów dostępnych dla korzystających ze sklepu. Za pomocą tej strony możemy zarządzać listą produktów dostępnych na naszej stronie. 

Jeżeli korzystamy z konta zwykłego użytkownika, nasze opcje są ograniczone tylko do 2 rzeczy.
Details -  wyświetla szczegóły produktu.
Add to Cart - dodaje wybrany produkt do koszyka.

Jeżeli zalogujemy się kontem administratora, oprócz wymienionych wyżej funkcji mamy dostępne 3 funkcje więcej.
Add Product - formularz zawierający pola "Product Name"(string), "Price"(decimal), "Stock"(int) oraz "Description"(string). Po poprawnym wypełnieniu tych pól możliwe jest dodanie nowego produktu do listy produktów.
Update - wyświetla wszystkie informacje dotyczące produktu z możliwością ich edytowania.
Delete - usuwa wybrany produkt z listy produktów.

Strona Cart - koszyk, wyświetla produkty z listy produktów dodane przez użytkownika do koszyka. Na tej stronie możemy zarządzać listą produktów dodanych do koszyka.
Dostępne opcje to zarządzanie ilością sztuk wybranego produktu do zamówienia oraz usuwanie wybranych produktów z koszyka. Jeżeli użytkownik będzie chciał zakupić większą ilość produtku niż jego ilość na magazynie, strona poinformuje o ilości dostępnych sztuk i za zgodą użytkownika automatycznie ją zmniejszy.

`Instalacja Projektu`


1. Zainstalowanie SQL Server Management Studio korzystając z podanego linku "https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16".

2. Pobranie projektu z GitHub z linku "https://github.com/Daniel3244/OnlineStoreApp".

3. Dodanie folderu "OnlineStoreApp-BackEnd" do Visual Studio 2022.

4. Uruchomienie rozwiązania poprzez uruchomienie pliku "OnlineStoreApp.sln".

5. Skonfigurowanie projektu startowego, rozwijając liste przy przycisku Rozpocznij -> Konfiguruj projekty startowe, wybieramy opcje Wiele projektów startowych oraz wybranie opcji "Uruchomienie" przy projekcie "OnlineStoreApp.ProductService","OnlineStoreApp.UserService","OnlineStoreApp.WebAPI".

6. W plikach "appsettings.json" w projektach "OnlineStoreApp.ProductService","OnlineStoreApp.UserService","OnlineStoreApp.WebAPI" należy zmienić nazwe serwera na nazwę, która wyświetla się przy uruchomieniu SQL Server Management Studio (np. LAPTOP-OJOJ9HFM).

7. Zainstalowanie najnowszej wersji .NET korzystając z linku "https://dotnet.microsoft.com/en-us/download/dotnet/8.0".

Poprawną instalacje .NET możemy zweryfikować korzystając z polecenia "dotnet --version".

8. Zainstalowanie "dotnet ef" korzystając z polecenia "dotnet tool install --global dotnet-ef". Poprawną instalacje możemy zweryfikować korzystając z polecenia "dotnet ef".

9. Zrobienie migracji do bazy danych korzystając z polecenia,

dotnet ef migrations add InitialCreate --context AppDbContext --project OnlineStoreApp.Infrastructure

Następnie zaktualizowanie bazy danych poleceniem,

dotnet ef database update --context AppDbContext --project OnlineStoreApp.Infrastructure

Po wykonaniu tych komend możemy zweryfikować czy na naszym komputerze została utworzona baza OnlineStoreDb z odpowiednimi tabelami.

10. Uruchomienie projektu w Visual Studio 2022.

11. Pobranie brancha "FrontEnd" z linku "https://github.com/Daniel3244/OnlineStoreApp/tree/FrontEnd".

12. Instalacja Node.js z linku "https://nodejs.org/en".

13. Przejście w CMD do folderu z wypakowanym folderem OnlineStoreApp-FrontEnd (przykładowa ścieżka to C:\Users\*nazwa użytkownika*\source\repos\OnlineStoreApp\OnlineStoreApp-FrontEnd).

14. Uruchomienie następujących komend: 

npm install 

npm start

15. Powinna otworzyć się przeglądarka z w pełni działającym Sklepem Internetowym.

`Paczki wykorzystane w projekcie:`

"Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.5"
"Microsoft.AspNetCore.OpenApi" Version="8.0.5"
"Microsoft.EntityFrameworkCore" Version="8.0.5"
"Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.5"
"Microsoft.EntityFrameworkCore.Tools" Version="8.0.5"
"coverlet.collector" Version="6.0.0"
"Microsoft.AspNetCore.Mvc.Testing" Version="8.0.6"
"Microsoft.EntityFrameworkCore.InMemory" Version="8.0.6"
"Microsoft.NET.Test.Sdk" Version="17.8.0"
"Mock" Version="1.0.0"
"Moq" Version="4.20.70"
"Serilog.AspNetCore" Version="8.0.1"
"Serilog.Extensions.Logging" Version="8.0.0"
"Serilog.Sinks.File" Version="6.0.0"
"xunit" Version="2.5.3"
"xunit.runner.visualstudio" Version="2.5.3"
"Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.5"
"Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.5"
"Microsoft.EntityFrameworkCore.Tools" Version="8.0.5"
"Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.5"
"Microsoft.AspNetCore.OpenApi" Version="8.0.5"
"Microsoft.EntityFrameworkCore" Version="8.0.5"
"Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.5"
"Microsoft.EntityFrameworkCore.Tools" Version="8.0.5"

`Autorzy Projektu`

Daniel Migas

Damian Mitka

Grzegorz Moskała

Kacper Mitana
