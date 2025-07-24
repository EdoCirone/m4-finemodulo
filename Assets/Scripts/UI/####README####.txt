Cartella UI

Contiene tutta la logica dell'interfaccia utente: HUD in-game, timer, sistema cuori, monete e menu.

---

UIManager.cs:

Gestisce l'interfaccia del gioco.  
Funge da ponte tra logica e elementi grafici: aggiorna vita, timer e monete.  
Espone metodi pubblici per gli altri sistemi (es. `UpdateLife()`, `UpdateCoins()`), e supporta il reset/rebind automatico all'avvio scena.

---

UI_CoinPanel.cs:

Aggiorna il testo delle monete raccolte.  
Mostra il formato `attuali / totali`.

---

UI_LifePanel.cs:

Sistema di cuori per visualizzare la vita.  
Instanzia dinamicamente cuori (`Image`) e ne cambia il colore tra pieno e vuoto.  
Supporta aggiornamenti dinamici e reset.

---

UI_Timer.cs:

Visualizza il tempo restante in formato `MM:SS`.  
Aggiorna il testo con `UpdateTimerUI()`.

---

MenuManager.cs:

Gestisce l'attivazione dei menu di gioco:

- Pausa
- Vittoria
- Game Over

Mette in pausa il tempo (`Time.timeScale = 0f`) quando necessario.  
Usato da `LifeController`, `MyInputs` o altri eventi di gioco.
