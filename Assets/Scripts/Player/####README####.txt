Cartella Player

Contiene tutta la logica specifica del personaggio giocante: movimento, animazioni, gestione della vita, input e respawn.

---

PlayerController.cs:

Script principale per il controllo del player.  
Supporta due modalità di movimento (`Normal`, `Tank`) e salti multipli.  
Gestisce il rilevamento del suolo, salti, rotazione e movimento con rigidbody.  
Espone eventi Unity per salto e atterraggio.

---

PlayerAnimatorController.cs:

Controlla l'animator del player aggiornando i parametri in tempo reale.  
Gestisce transizioni, animazioni di hit, salto e morte.  
Si collega direttamente al `PlayerController` e al `Rigidbody`.

---

LifeController.cs:

Gestisce la salute del player e le condizioni di morte.  
Supporta danno da caduta (`fallDeath`), danno normale, respawn e cura.  
Integra pausa temporanea per animazioni e feedback visivi.  
Si collega a `UIManager` e `MenuManager`.

---

PlayerRespawn.cs:

Implementa l'interfaccia `IRespawnable`.  
Permette di spostare il player al checkpoint attuale o alla posizione iniziale.  
Si occupa anche di azzerare la fisica tramite Rigidbody.

---

PlatformAttachmentHandler.cs:

Gestisce l’attaccamento del player a piattaforme mobili.  
Usa il delta della piattaforma (`DeltaPosition`) per spostare il player in modo fluido.  
Si collega a `TranslationMovement`.

---

MyInputs.cs:

Sistema minimale per intercettare l’input Escape e lanciare un evento.  
Può essere usato per aprire menu, mettere in pausa, ecc.
