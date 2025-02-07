Se till att använda Factories i dina services. 





Att tänka på:

Du använder dig av en enum för att hantera status på ett projekt. Detta är inte att rekommendera. Detta skulle innebära att varje gång vi vill ändra något, som att byta namn, lägga till fler eller tar bort något så skulle vi behöva bygga om hela applikationen (new build). Det gör också att det blir utvecklarens jobb att hantera vilket blir kostsamt. Så detta ska egentligen hanteras som en egen entitet/tabell i databasen.



Du gör throw exceptions. Detta är inte att rekommendera. Visst det är många utvecklare som gör det men det är enormt resurskrävande och ökar risken för att du kraschar systemet om du inte har garanterat att ALLA exceptions hanteras. Så försök att inte göra detta. Ett mycket bättre alternativ är att använd sig av en svarsklass/svarsrecord såsom IResult, TResult, HttpResponse, eller bygger eget. Något att tänka på framöver.



I din BaseRepository har du GetByIdAsync, men alla entiteter har inte ett id som en int vilket gör att den inte ska ligga i BaseRepository utan det är i såfall något som ska ligga i en enskild repository. Och varför hanterar du inte exceptions här? Det är det här jag menar med det ja skrev innan. Om du ska använda dig av throw exception så är det viktigt att du hanterar det överallt. Annars finns det stora risker att man kraschar applikationen som sagt.



I dina services varför använder du inte factories? nu mappar du om allt i din service istället för att hantera det i factories.



Sen gör du then throw exception i en async-metod. Inte bra! du måste iså fall använda dig av cancellation tokens så att du faktiskt avbryter själva tråden också. Nu läcker ditt applikation minne.