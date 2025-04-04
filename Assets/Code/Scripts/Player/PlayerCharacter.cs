using UnityEngine;

public class PlayerCharacter : BaseTower
{
    public float waveRadius;
    new void Shoot()
    {
        if (target == null) return;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, target.position - firingPoint.position);
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, rotation);

        BaseBullet bulletScript = bullet.GetComponent<BaseBullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(target, damage);
        }
    }
    public void EmitDecipheringWave()
    {
        GameObject wave = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        AlanTuringWave turingWaveScript = wave.GetComponent<AlanTuringWave>();
        turingWaveScript.SetProperties(waveRadius, enemyMask);
    }
}
