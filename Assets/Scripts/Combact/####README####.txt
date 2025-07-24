Cartella Combat

Contiene gli script legati al sistema di combattimento e alle interazioni che infliggono danni.  
Include la logica base per danno, cura, proiettili, armi e bombe.

---

MakeDamage.cs:

Componente da attaccare a oggetti che infliggono danno su collisione o trigger.  
Comunica l’impatto tramite evento (`OnHit`) e può disattivarsi dopo il colpo.

---

Healer.cs:

Oggetto che cura il giocatore quando lo tocca.  
Chiama `AddHp()` su `LifeController` e si autodistrugge dopo l’uso.

---

Weapons/

Contiene script per torrette o entità che sparano proiettili.

- `BasicShooter`: controlla distanza dal player e spara proiettili dal pool.
- `AimShooter`: estende `BasicShooter` e ruota verso il player.
- `Bullet`: gestisce movimento e ciclo di vita del proiettile. Compatibile con `MakeDamage` e `PoolManager`.

---

Explosive/

Contiene gli script relativi a bombe reattive.

- `Bomb`: rileva il player, avvia un countdown visivo e infligge danno nell’area.
