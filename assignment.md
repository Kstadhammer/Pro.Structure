nlämningsuppgift - Datalagring


I denna inlämningsuppgift ska du bygga en applikation som kommunicerar med en databas med hjälp av C# och .NET. Du väljer själv vilken applikationsform du vill använda: Console, WPF/Avalonia WPF, MAUI, eller React. Varje applikationsplattform har sina egna styrkor och utmaningar. Det är ditt ansvar att utvärdera vilken plattform som bäst uppfyller kraven för godkänt eller väl godkänt.



Observera att vissa plattformar, exempelvis React eller MAUI, kan kräva att du implementerar ett Web API (med ASP.NET Core) för att hantera kommunikationen mellan frontend och backend. Du får använda ett Web API oavsett plattform, men det är inget krav och påverkar inte betygsättningen.



Användning av AI-genererad kod
Om du använder kod som genererats av AI (exempelvis ChatGPT), måste detta tydligt framgå för att undvika plagiat.



Val av lagringslösning
Du väljer själv vilken typ av databasmotor du vill använda, exempelvis: Sqlite, LocalDB eller SQL Server. Välj en lösning som passar bäst för din plattform och ditt ändamål. Observera följande: Undvik att lagra känslig information, såsom personnummer, kortuppgifter eller riktiga lösenord, i databasen; Du behöver inte ladda upp din databas till GitHub.



Design och inspiration
Systemet ska vara strukturerat enligt moderna principer och uppdelat i entities, models, repositories och services. SOLID-principerna ska tillämpas där det är lämpligt. Du får själv välja hur du vill att din applikation ska se ut, det grafiska utseendet bedöms inte, men du kan använda inspiration från Fortnox.










INFORMATION OM PROJEKTUPPGIFTEN
Företaget Mattin-Lassei Group AB vill ha ett system för att hantera sina projekt. Du ska bygga en applikation där användare kan lägga till projekt med uppgifter som projektnummer (t.ex. "P-101"), namn, tidsperiod (start- och slutdatum), projektansvarig, kund, tjänst (t.ex. konsulttid 1000 kr/tim), totalpris och status (ej påbörjat, pågående, avslutat). Projektnumret ska genereras automatiskt och får inte ändras efter att projektet skapats.



Applikationen ska visa en översikt med alla aktuella projekt och deras projektnummer, namn, tidsperiod och status. Det ska finnas möjlighet att klicka på ett projekt för att komma till en detaljerad vy där alla uppgifter (utom projektnumret) kan redigeras. Användaren ska kunna spara ändringar eller avbryta redigeringen och navigera tillbaka till översikten.





BETYGSKRITERIER FÖR INLÄMNINGSUPPGIFTEN
Här nedan specificeras vad som krävs för att uppnå godkänt respektive väl godkänt. Det är viktigt att notera att det är ett stort steg mellan dessa nivåer, vilket följer de riktlinjer som EC-Utbildning nu tillämpar.



FÖR GODKÄNT KRÄVS FÖLJANDE:

Frontend-baserad applikation enligt specifikationen ovan med följande:

En sida som listar alla befintliga projekt.
En sida där man kan skapa ett nytt projekt.
En sida där man kan editera/uppdatera ett befintligt projekt.


Ett klassbibliotek med följande:

Använda Entity Framework Core - Code First.
Använda valfri databaslösning av de som presenterats i kursen/uppgiften.
Använda minst två olika entiteter/tabeller (såsom kund och projekt).
Använda Services för att hantera projekt och kunder.
Använda Repositories för att hantera alla entiteter/tabeller.
Använda Dependency Injection.
Tillämpa S i SOLID (Single Responsibility Principle). (Övriga SOLID-principer är inte ett krav för godkänt.)


FÖR VÄL GODKÄNT KRÄVS FÖLJANDE:

Frontend-baserad applikation enligt G-kraven.



Ett klassbibliotek enligt G-kraven med följande utökning:

Använda Repositories som baseras på en BaseClass (Generic).
Använda fler entiteter/tabeller (såsom kund, projektansvarig, projekt, tjänst etc.).
Använda Services för att hantera alla entiteter på ett lämpligt sätt, vilket innebär att man ska kunna hantera CRUD för kunder, projektansvarig, projekt etc.
Tillämpa SOLID-principer där det är lämpligt.
Tillämpa Factories.
Task ska tillämpas där det är lämpligt genom att använda asynkrona metoder och async/await, t.ex. vid databasoperationer.
Tillämpa Transaction Management för att hantera flera beroende operationer som en transaktion, inklusive rollback vid fel.
