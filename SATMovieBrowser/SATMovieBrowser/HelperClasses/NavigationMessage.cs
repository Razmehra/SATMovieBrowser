using System;
using System.Collections.Generic;
using System.Text;

namespace SATMovieBrowser.HelperClasses
{
    public class NavigationMessage
    {

        #region public properties

        public object Options { get; set; }
        public object Options2 { get; set; }

        #endregion

        #region ctor

        public NavigationMessage() : this(null) { }

        public NavigationMessage(object obj) : this(obj, null) { }

        public NavigationMessage(object options, object options2)
        {
            Options = options;
            Options2 = options2;
        }

        #endregion

    }
}
