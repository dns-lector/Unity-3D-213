using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            // GameState.flashCharge = 1.0f;
            GameState.TriggerEvent("Battery", Random.Range(0.5f, 1.0f));
            Destroy(gameObject);
        }
    }
}
/* �.�. ���������� ��������� �������� ������ "���������" - 
 * ��� ������� � ������� ����� ������������ �� ����'������ �� 1,
 * � �� ������� (���������) ����� � �������� 0,75-1,0
 * ��������� ������ ��������� �� ����, ������� ��� ������� �����
 * �����, ��� ��������� �� ���� �� ����.
 */
