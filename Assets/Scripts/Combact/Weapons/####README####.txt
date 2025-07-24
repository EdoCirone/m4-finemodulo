Cartella Weapons

Contiene script legati a nemici o torrette che sparano proiettili.  
Il sistema è pensato per essere compatibile con il `PoolManager`.

---

BasicShooter.cs:

Componente base per entità che sparano proiettili.  
Controlla la distanza dal player e la frequenza di sparo.  
Crea proiettili tramite `PoolManager` e li spara nella direzione del `bulletSpawner`.

---

AimShooter.cs:

Estende `BasicShooter`.  
Ruota automaticamente una parte (es. testa o torretta) verso il player prima di sparare.

---

Bullet.cs:

Proiettile base compatibile con il pooling.  
Si muove in linea retta e si disattiva dopo un certo tempo o quando infligge danno.  
Richiede il componente `MakeDamage`.
s