using UnityEngine;

namespace Infrastructure.Services
{
    public class CursorLockService
    {
        public void Show() => Cursor.visible = true;
        public void Hide() => Cursor.visible = false;
    }
}