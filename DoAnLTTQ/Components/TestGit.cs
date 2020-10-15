using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnLTTQ.Components
{
    public partial class TestGit : Component
    {
        public TestGit()
        {
            InitializeComponent();
        }

        public TestGit(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
