Cartella Spawn

Contiene la logica per il sistema di respawn del giocatore o altri oggetti.  
Il modulo è pensato per essere flessibile, con supporto a checkpoint e interfacce per collegarlo ad altri sistemi.

---

IRespawnable.cs:

Interfaccia semplice per oggetti che possono essere respawnati in un punto specifico.  
Deve essere implementata da oggetti che supportano la chiamata `RespawnHere()`.

---

Respawnable.cs:

Componente generico che permette a un oggetto di respawnare all’ultimo checkpoint attivo.  
Richiede la presenza del `CheckpointManager` nella scena.


Checkpoints/

Contiene la logica di checkpoint per il player.  
Permette di aggiornare dinamicamente il punto di respawn attuale.

---

Checkpoint.cs:

Trigger da posizionare nella scena.  
Quando il player lo attraversa:

- Imposta il transform come nuovo spawn point.
- Attiva un feedback visivo (es. bandiera o effetto).

---

CheckpointManager.cs:

Sistema singleton che tiene traccia del checkpoint attuale.  
Espone metodi per ottenere il transform del punto attivo (`GetCurrentCheckpoint`) e verificarne la presenza.
