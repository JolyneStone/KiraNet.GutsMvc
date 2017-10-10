using System.Threading;

namespace KiraNet.GutsMvc.Helper
{
    /// <summary>
    /// 简单自旋锁
    /// </summary>
    public struct KiraSpinLock
    {
        /// <summary>
        /// 资源是否被占用
        /// </summary>
        private int _isResouceUse; // 0 = false（默认）; 1 = true

        public void Enter()
        {
            while (true)
            {
                // 将_isResouceUse设置为1，并检查原先的值是否为0
                // 如果条件成立，则资源可以使用
                // 否则该资源已经被占用
                if (Interlocked.Exchange(ref _isResouceUse, 1) == 0)
                    return;
            }
        }

        public void Exit()
        {
            Volatile.Write(ref _isResouceUse, 0);
        }
    }
}
