# Klein Hotel Amersfoort

## Inleiding
Een klein hotel gevestigd in Amersfoort heeft een website waar overnachtingen op geboekt kunnen worden. Naast deze functionaliteit zouden ze graag willen zien dat er door gasten ook reacties achter kunnen worden gelaten in een gastenboek nadat ze overnacht hebben in het hotel. 
De frontend die hierbij hoort is al gerealiseerd, maar de backend nog niet.

## Opdracht
Realiseer het backend gedeelte voor het gastenboek. Deze moet de volgende functionaliteiten omvatten:
- Reacties opslaan;
- Reacties ophalen;
- Reacties verwijderen.

De backend van de website heeft als basis _.NET Core Web API_ en levert een _REST-interface_ die benaderd kan worden d.m.v. JSON-berichten.

## Reacties opslaan
Een gebruiker die in het hotel heeft overnacht moet een reactie kunnen achter laten in het gastenboek van het hotel. Om dit te kunnen doen, moet de gebruiker ingelogd zijn op de website.
Om dit te valideren moet er gebruik gemaakt worden van een token verificatie.

De _body_ van de _request_ bestaat uit de `Reaction` class zoals hier naast afgebeeld. Hierbij worden zowel het `Id` als de `Date` ingevuld bij het moment van opslaan en wordt de `Name` bepaald aan de hand van info die bij het door gestuurde authenticatie token hoort.

### Voorbeeld
**Request**
```http
POST /reactions HTTP/1.1
Content-Type: application/json

{
    "name": "Renée Rekers",
    "text": "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
    "score": 2,
    "email": "ReneeRekers@armyspy.com",
    "token": "KVU84DGK9RK"
}
```
**Response**
```http
Reaction saved!
```

## Reacties verwijderen
Soms zitten er reacties tussen die niet gewenst zijn om op de website te tonen, en deze zullen dan ook verwijderd moeten kunnen worden. Echter, niet iedereen kan dit zomaar doen, alleen admins vandewebsitemogenditdoen. Op basis van de token moet achterhaald kunnen worden of iemand het recht `AllowRemoveReactions` heeft. 
Verwijderen dient te gebeuren via een gangbare REST call om items te verwijderen.

### Voorbeeld
**Request**
```http
DELETE /reactions HTTP/1.1
Content-Type: application/json

{
    "Id": "944fd606-d8df-4a39-980b-6e530e66a457",
    "token": "KFA71KER8SS"
}
```
**Response**
```http
Reaction deleted!
```

## Reacties ophalen
Iedere gebruiker van de website moet reacties in kunnen zien, ook de gebruikers die niet ingelogd zijn. Bij deze request zijn er een aantal parameters die meegeven kunnen worden in de URL om de reacties te sorteren:
- **count**: hetaantalreactiesdatgetoondmoetworden.Indiennietmeegegeven,dan is de standaardwaarde hiervan **5**. Indien het aantal groter is dan het beschikbare aantal reacties, dan geeft hij alle reacties.
- **sort**: de waarde waarop hij op moet sorteren. Dit kan **date** of **score** zijn. Indien niet meegegeven, dan is de standaardwaarde hiervan **date**.
- **order**: de sorteervolgorde.Deze kan **asc** of **desc** zijn. Indien niet meegeven, dan is de standaard waarde hiervan **desc**.

Verder is het hierbij belangrijk dat alleen een admin de emailadressen kan zien, deze is voor andere gebruikers niet zichtbaar.

### Voorbeeld
**Request**
```http
GET /reactions/2/score HTTP/1.1
Accept: application/json
Token: KFA71KER8SS
```
**Response**
```json
[
    {
        "id": "68fe802e-55a8-4315-9277-2fb369eb0cb8",
        "name": "Randolf de Kruif",
        "text": "Aenean vitae condimentum magna. Aenean ligula nibh, dapibus vitae pretium at, convallis a dolor. Nunc venenatis ex quis ante feugiat, nec blandit ipsum dignissim.",
        "date": "2019-02-12T00:00:00+01:00",
        "score": 4,
        "email": "RandolfdeKruif@jourrapide.com"
    },
    {
        "id": "b30e5473-7956-4ba3-b16b-8014309f6774",
        "name": "Dayenne Visser",
        "text": "Suspendisse id tempus erat. Donec ac diam at sem faucibus ornare vitae quis nisl.",
        "date": "2018-10-23T00:00:00+02:00",
        "score": 3,
        "email": "DayenneVisser@teleworm.us"
    }
]
```