using System.Windows.Forms;

namespace STO.Print.Utilities
{
    public class CheckBoxListUtil
    {
        public static void SetCheck(CheckedListBox cblItems, string valueList)
        {
            string[] strtemp = valueList.Split(',');
            foreach (string str in strtemp)
            {
                for (int i = 0; i < cblItems.Items.Count; i++)
                {
                    if (cblItems.GetItemText(cblItems.Items[i]) == str)
                    {
                        cblItems.SetItemChecked(i, true);
                    }
                }
            }
        }

        public static string GetCheckedItems(CheckedListBox cblItems)
        {
            string resultList = "";
            for (int i = 0; i < cblItems.CheckedItems.Count; i++)
            {
                if (cblItems.GetItemChecked(i))
                {
                    resultList += string.Format("{0},", cblItems.GetItemText(cblItems.Items[i]));
                }
            }
            return resultList.Trim(',');
        }
    }
}
