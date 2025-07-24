Cartella SystemUtils

Contiene utility generiche di sistema, utilizzate trasversalmente in tutto il progetto.

---

SceneLoader.cs:

Singleton per la gestione del caricamento delle scene.  
Espone metodi per:

- Ricaricare il livello corrente.
- Passare al livello successivo.
- Tornare al menu principale.
- Caricare scene per indice o nome.
- Chiudere il gioco.

Si occupa anche di reinizializzare la UI e ricollegare il player alla camera dopo un cambio scena.

---

DontDestroy.cs:

Componente da mettere su GameObject che devono persistere tra le scene.  
Se l’oggetto ha un parent, segnala che non verrà salvato (Unity limita `DontDestroyOnLoad` ai root).
