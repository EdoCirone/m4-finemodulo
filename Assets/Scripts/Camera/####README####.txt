Cartella Camera

Questa cartella contiene gli script legati alla gestione della telecamera.  
Al momento è presente un solo script, ma il file è pronto per eventuali estensioni future (shake, transizioni, inquadrature dinamiche, ecc).

---

CameraManager.cs:

Gestisce il movimento della telecamera in terza persona, seguendo il giocatore.

- Il pivot orizzontale (`_cameraPivot`) ruota attorno all'asse Y.
- Il nodo figlio (`_cameraPitch`) gestisce la rotazione verticale (pitch), con limiti configurabili.
- Si attiva con `MouseButton(0)` per ruotare liberamente la camera.
- Segue la posizione del player ogni frame.

Include anche un hook automatico per cercare il player nella scena quando viene caricata (`tag == Player`).

---

Note generali:

- Le sensibilità sono modificabili da Inspector.
- Il comportamento è pensato per un controller 3D semplice e responsivo.
- L'inquadratura può essere migliorata aggiungendo smoothing o ostacoli.

