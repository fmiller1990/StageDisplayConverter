using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StageDisplayConverter.Helpers

{
    /// <summary>
    /// Base Class for the Model - View- ViewModel approach
    /// </summary>
    [Serializable]
    public class MVVMBase : Object, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// throwas a PropertyChanged Evend
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }


    }
}


