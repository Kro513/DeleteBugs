using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{ 
	protected override void NextMoveTo()
	{
		if (currentIndex < wayPointCount - 1)
		{
			transform.position = wayPoints[currentIndex].position;
			currentIndex++;
			Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
			movement2D.MoveTo(direction);

			if (direction != Vector3.zero)
			{
				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				angle -= 90.0f;
				transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			}
		}
		else
		{
			GameManager.Instance.player.GetDamage(10);
			OnDie();

			//Destroy(gameObject);
		}
	}
}
