
Questa cartella contiene tutti gli script C# del progetto, organizzati in sottocartelle per area funzionale.

Ogni sottocartella rappresenta un dominio logico del gioco (camera, player, danno, ecc). L'obbiettivo è avere script modulari, chiari e facilmente riutilizzabili.

  Camera
Contiene gli script che gestiscono il comportamento della telecamera.

  Collectables
Gestisce i collezionabili e il loro tracking.

 Core
Include classi fondamentali e manager generici (es: pooling system).

 DamageSystem
Tutto ciò che riguarda danno, cura, proiettili, esplosivi e logiche di combattimento.

  Movement
Script che controllano movimenti modulari di piattaforme e oggetti.

  Player
Contiene gli script del giocatore: movimento, vita, abilità.

   Triggers
Contiene trigger di gioco (es: fine livello, checkpoint, trappole).

## Naming ##

- Classi: PascalCase (`PlayerController`, `Bullet`)
- File: devono avere lo stesso nome della classe
- Campi privati: `_camelCase`


