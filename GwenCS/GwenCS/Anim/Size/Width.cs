﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gwen.Anim.Size
{
    class Width : TimedAnimation
    {
        protected int m_iStartSize;
        protected int m_iDelta;
        protected bool m_bHide;

        public Width(int iStartSize, int iEndSize, double fLength, bool bHide = false, double fDelay = 0.0, double fEase = 1.0)
            : base(fLength, fDelay, fEase)
        {
            m_iStartSize = iStartSize;
            m_iDelta = iEndSize - m_iStartSize;
            m_bHide = bHide;
        }

        public override void OnStart() { m_Control.Width = m_iStartSize; }
        public override void Run(double delta) { m_Control.Width = (int)Math.Round(m_iStartSize + (m_iDelta * delta)); }
        public override void OnFinish() { m_Control.Width = m_iStartSize + m_iDelta; m_Control.IsHidden = m_bHide; }
    }
}
