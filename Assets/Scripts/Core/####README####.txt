Cartella Core

Contiene i sistemi fondamentali che regolano il comportamento generale del gioco.  
È divisa in sottocartelle per mantenere separati i diversi ambiti funzionali.

---

Pooling/

Sistema per riutilizzare oggetti in modo efficiente.  
Evita continue instanziazioni e distruzioni, utile per effetti, proiettili, nemici, ecc.

---

Spawn/

Gestisce i checkpoint e la logica di respawn.  
Include interfacce per collegare player e nemici a punti di rinascita personalizzati.

---

GameLogic/

Contiene script legati alle regole generali del livello, come il timer e il punto di arrivo.

---

SystemUtils/

Utility trasversali usate ovunque, come il caricamento scene e la persistenza tra scene.

---

Tutti gli script sono progettati per essere riutilizzabili e indipendenti, con interfacce o singleton quando serve.
