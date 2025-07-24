Cartella Collectables

Contiene la logica per i collezionabili nella scena.  
Il sistema tiene traccia di quelli raccolti dal player e permette di eseguire eventi al momento della raccolta (singola o completa).

---

Collectable.cs:

Script da mettere sull’oggetto collezionabile (es. moneta, cristallo, reliquia).  
Rileva il contatto con il layer del player e comunica al `CollectableManager` che è stato raccolto.  
Dopo il contatto, distrugge se stesso.

---

CollectableManager.cs:

Gestisce l’intero sistema di raccolta:

- Tiene traccia dei collezionabili nella scena (`_collectablesInScene`).
- Tiene traccia di quelli raccolti dal giocatore (`_playerCollectables`).
- Aggiorna l'interfaccia utente tramite `UIManager`.
- Attiva eventi Unity alla raccolta (`_onCollected`) e quando sono stati presi tutti (`_onCollectedall`).
- Mostra il `finishPoint` alla fine.
- I collectables vengono **rilevati automaticamente all'avvio**, usando `FindObjectsOfType<Collectable>()`, quindi basta piazzarli nella scena da editor.
