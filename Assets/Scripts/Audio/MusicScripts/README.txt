Cartella MusicScripts

Questa sottocartella contiene gli script per la gestione della musica di sottofondo.

MusicManager.cs`:

Un Sistema singleton che:

Capisce se cambia la scena
Riproduce la musica corretta per la scena corrente, cercandolo in una lista associativa `sceneName → musicClip`.
Gestisce la configurazione base dell'AudioSource (volume, spatial blend, ecc.).
Lo script presuppone che gli AudioClip siano impostati via Inspector. Assicurati che `MusicManager` sia presente nella scena.

