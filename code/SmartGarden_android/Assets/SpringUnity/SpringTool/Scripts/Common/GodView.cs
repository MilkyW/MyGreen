using UnityEngine;

namespace SpringFramework
{
    public class GodView : MonoBehaviour
	{
		public float MoveSpeed = 5.0f;
		public float RotateSpeed = 4.0f;
		private float _shiftSpeed = 0.0f;

        private void Update()
		{
			AddSpeed();
			ControllerMove();
            ControllerView();
        }
		
		private void AddSpeed()
		{
			if (Input.GetKey(KeyCode.LeftShift)) _shiftSpeed = MoveSpeed * 2f;
			else _shiftSpeed = MoveSpeed;
		}

        private void ControllerView()//按下鼠标右键控制视角
        {
            if (Input.GetMouseButton(1))
            {
                transform.RotateAround(transform.position , Vector3.up , RotateSpeed * Input.GetAxis("Mouse X"));
                transform.RotateAround(transform.position , transform.right , -RotateSpeed * Input.GetAxis("Mouse Y"));
            }
        }

        private void ControllerMove()
		{
            if (Input.GetKey(KeyCode.A))MoveTo(Vector3.left);
            if (Input.GetKey(KeyCode.D))MoveTo(Vector3.right);
            if (Input.GetKey(KeyCode.W))MoveTo(Vector3.forward);
            if (Input.GetKey(KeyCode.S))MoveTo(Vector3.back);
            if (Input.GetKey(KeyCode.Q))MoveTo(Vector3.down);
            if (Input.GetKey(KeyCode.E))MoveTo(Vector3.up);
        }
		
		private void MoveTo(Vector3 direction)
		{
			transform.Translate(direction * _shiftSpeed * Time.deltaTime);
		}
	}
}