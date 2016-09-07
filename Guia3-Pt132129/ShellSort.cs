using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guia4_Pt132129
{
    public class ShellSort
    {
        public void Sort(int[] list) {

            int j, inc;

            inc = list.Length / 2;

            while (inc >= 1) {

                for (int i = inc; i < list.Length; i++) {

                    int v = list[i];

                    j = i - inc;

                while (j >= 0 && list[j] > v) {

                    list[j + inc] = list[j];

                    j = j - inc;
                }

                list[j + inc] = v;
            }
                inc = inc / 2;

            }
        }
    }
}
