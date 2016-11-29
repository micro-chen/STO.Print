
namespace STO.Print.Utilities
{
    /// <summary>
    /// 各种常用数组排序操作辅助类
    /// </summary>
    public class SortHelper
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="list">要排序的对象数组list</param>
        /// <param name="key">排序关键字key</param>
        /// <param name="isReverse">做为反排序的标志位</param>
        //public static void Sort(T[] list, string key, bool isReverse)
        //{
        //    if (isReverse)
        //    {
        //        Array.Reverse(list);
        //        isReverse = false;
        //    }
        //    else
        //    {
        //        int len = list.Length;
        //        Type type = typeof(T);
        //        object[] keys = new object[len];
        //        for (int i = 0; i < len; i++)
        //        {
        //            keys[i] = type.InvokeMember(key, BindingFlags.GetField, null, list[i], null);
        //        }
        //        Array.Sort(keys, list);
        //        isReverse = true;
        //    }
        //}

        /// <summary>
        /// 冒泡排序法1
        /// </summary>
        /// <param name="list"></param>
        public static void BubbleSort(int[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                for (int j = i; j < list.Length; j++)
                {
                    if (list[i] < list[j])
                    {
                        int temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// 插入排序法
        /// </summary>
        /// <param name="list"></param>
        public static void InsertionSort(int[] list)
        {
            for (int i = 1; i < list.Length; i++)
            {
                int t = list[i];
                int j = i;
                while ((j > 0) && (list[j - 1] > t))
                {
                    list[j] = list[j - 1];
                    --j;
                }
                list[j] = t;
            }

        }

        /// <summary>
        /// 选择排序法
        /// </summary>
        /// <param name="list"></param>
        public static void SelectionSort(int[] list)
        {
            int min;
            for (int i = 0; i < list.Length - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < list.Length; j++)
                {
                    if (list[j] < list[min])
                        min = j;
                }
                int t = list[min];
                list[min] = list[i];
                list[i] = t;
            }

        }

        /// <summary>
        /// 希尔排序法
        /// </summary>
        /// <param name="list"></param>
        public static void ShellSort(int[] list)
        {
            int inc;
            for (inc = 1; inc <= list.Length / 9; inc = 3 * inc + 1) ;
            for (; inc > 0; inc /= 3)
            {
                for (int i = inc + 1; i <= list.Length; i += inc)
                {
                    int t = list[i - 1];
                    int j = i;
                    while ((j > inc) && (list[j - inc - 1] > t))
                    {
                        list[j - 1] = list[j - inc - 1];
                        j -= inc;
                    }
                    list[j - 1] = t;
                }
            }
        }

        private static void Swap(ref int l, ref int r)
        {
            int s;
            s = l;
            l = r;
            r = s;
        }

        /// <summary>
        /// 快速排序法
        /// </summary>
        /// <param name="list"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        public static void QuickSort(int[] list, int low, int high)
        {
            int pivot;
            int l, r;
            int mid;
            if (high <= low)
            {
                return;
            }
            else if (high == low + 1)
            {
                if (list[low] > list[high])
                    Swap(ref list[low], ref list[high]);
                return;
            }
            mid = (low + high) >> 1;
            pivot = list[mid];
            Swap(ref list[low], ref list[mid]);
            l = low + 1;
            r = high;
            do
            {
                while (l <= r && list[l] < pivot)
                    l++;
                while (list[r] >= pivot)
                    r--;
                if (l < r)
                    Swap(ref list[l], ref list[r]);
            } while (l < r);
            list[low] = list[r];
            list[r] = pivot;
            if (low + 1 < r)
            {
                QuickSort(list, low, r - 1);
            }
            if (r + 1 < high)
            {
                QuickSort(list, r + 1, high);
            }
        }
    }
}
