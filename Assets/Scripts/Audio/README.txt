Cartella Audio

Questa cartella contiene tutta gli script legati alla gestione dell'audio, separata in due sottoinsiemi principali:

MusicScripts: gestione della musica di sottofondo, con caricamento automatico in base alla scena.
SFXScripts: gestione degli effetti sonori, sia per la UI che per il gameplay 3D/2D.


Struttura
-AudioType gestisce i tipi di audio (contiene due classi)
	- `SceneMusic`: associa una scena a un brano musicale.
	- `NamedSFX`: associa un nome (stringa) a un effetto sonoro.
- `MusicScripts/`: logica per la musica di sottofondo.
- `SFXScripts/`: logica per effetti sonori ambientali e UI.

Tutti gli script sono pensati per essere modulabili: gli AudioClip vengono assegnati da Inspector usando liste serializzate.
