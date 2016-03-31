using System;
using System.Collections.Generic;
using System.Text;
using DCRF.Common.Attributes;
using DCRF.Common.Core;
using DCRF.Common.Interface;
using AForm.Schema;
using DCRF.Common.Primitive;
using DCRF.Common.Definition;

namespace AForm.Builder
{
    [BlockId("FormBuilder")]
    public class FormBuilder: BlockBase
    {
        public FormBuilder(Guid id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void InitBlock()
        {
            base.InitBlock();

            innerWeb = new BlockWeb("temp", blockWeb.Broker, PlatformType.Neutral, this, false);
        }

        [BlockService]
        public Guid BuildWinForm(FormSchema form)
        {
            Guid winId = innerWeb.AddBlock(CID.New("WinForm"));
            innerWeb[winId].ProcessRequest("InitForm", form);

            return winId;
        }
    }
}
