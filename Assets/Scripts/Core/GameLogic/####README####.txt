Cartella GameLogic

Contiene la logica legata alle condizioni generali di vittoria e gestione del tempo.  
È pensata per gestire meccaniche trasversali al gameplay.

---

TimerManager.cs:

Gestisce un conto alla rovescia (es. per livelli a tempo).  
Alla scadenza:

- Aggiorna la UI.
- Invoca un evento Unity (`_onTimeExpire`).

---

FinishPoint.cs:

Trigger da posizionare nel punto di fine livello.  
Quando il player lo raggiunge, invoca l’evento `whenWin`.  
Utile per attivare menu di fine livello, salvataggi, transizioni, ecc.
