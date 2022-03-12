# ChatApp

## Scénář

Jednoduchá chatovací aplikace, která umoňuje komunikovat mezi uživateli v rámci místností současně v reálném čase. Při prvním příchodu do aplikace se uživatel pomocí jednoduchého formuláře zaregistruje. Po registraci je umožněno uživateli se pomocí emailu a hesla přihlásit. Následně po přihlášení je uživateli zobrazen seznam již existujících předdefinových místností, ve kterých bude probíhat veškerá komunikace. Při kliknutí na buňku s místností je otevřen seznam zpráv, kde je též textové pole pro odeslání zpráv v reálném čase. Pokud je uživatel právě na zmíněném seznamu zpráv, dostává v reálném čase zprávy od všech osob, které ve vybrané místnosti právě komunikují.

## Požadavky

- Uživatel se může registrovat do aplikace
    - Povinné údaje jsou *jméno*, *příjmení*, *email* a *heslo*
- Uživatel se může přihlásit do aplikace
    - Pomocí *emailu* a *hesla*
- Každý uživatel vidí seznam místností
    - Zobrazen *titulek* a krátký *popis* místnosti
- Uživatel vidí v místnosti seznam zpráv od všech uživatelů
    - U každé zprávy je zobrazeno *celé jméno* *autora* zprávy, samotná *zpráva* a *čas odeslání* zprávy
    - Zprávy, které patří *aktuálně přihlášenému* *uživateli* jsou na *pravé straně*
    - Zprávy, které patří *ostatním uživatelům* jsou na *levé straně*
- Uživatel může v místnosti posílat zprávy
    - Je dostupné *textové pole* a *tlačítko* pro odeslání zprávy
- Uživatel dokáže odesílat zprávy v reálném čase
- Uživatel dokáže přijímat zprávy v reálném čase

## Technologie

- .NET 6
- Razor
- SignalR
- Docker
- MongoDB

## Časový plán

- Plánování + založení .NET projektu + příprava technologií + příprava databázové struktury
- Základní struktura .NET projektu (projekty, modely, repository, service, ...)
- Implementace registrace a přihlášení
- Implementace zobrazení seznamu místností
- Implementace zobrazení seznamu zpráv ve vybrané místnosti
- Integrace SignalR do projektu
- Implementace odesílání zprávy pomocí SignalR
- Implementace přijímání zpráv pomocí SignalR
- Úprava vzhledu aplikace
- Refactoring + testování
