/****************************************************************************
Copyright (c) 2010-2012 cocos2d-x.org
Copyright (c) 2008-2010 Ricardo Quesada
Copyright (c) 2011 Zynga Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/

namespace cocos2d
{
    public class CCTransitionZoomFlipX : CCTransitionSceneOriented
    {
        public override void OnEnter()
        {
            base.OnEnter();

            CCActionInterval inA, outA;
            m_pInScene.Visible = false;

            float inDeltaZ, inAngleZ;
            float outDeltaZ, outAngleZ;

            if (m_eOrientation == tOrientation.kOrientationRightOver)
            {
                inDeltaZ = 90;
                inAngleZ = 270;
                outDeltaZ = 90;
                outAngleZ = 0;
            }
            else
            {
                inDeltaZ = -90;
                inAngleZ = 90;
                outDeltaZ = -90;
                outAngleZ = 0;
            }
            inA = CCSequence.FromActions
                (
                    new CCDelayTime (m_fDuration / 2),
                    CCSpawn.FromActions
                        (
                            new CCOrbitCamera(m_fDuration / 2, 1, 0, inAngleZ, inDeltaZ, 0, 0),
                            new CCScaleTo(m_fDuration / 2, 1),
                            CCShow.Create()
                        ),
                    CCCallFunc.Create(Finish)
                );
            outA = CCSequence.FromActions
                (
                    CCSpawn.FromActions
                        (
                            new CCOrbitCamera(m_fDuration / 2, 1, 0, outAngleZ, outDeltaZ, 0, 0),
                            new CCScaleTo(m_fDuration / 2, 0.5f)
                        ),
                    CCHide.Create(),
                    new CCDelayTime (m_fDuration / 2)
                );

            m_pInScene.Scale = 0.5f;
            m_pInScene.RunAction(inA);
            m_pOutScene.RunAction(outA);
        }

        public new static CCTransitionZoomFlipX Create(float t, CCScene s, tOrientation o)
        {
            var pScene = new CCTransitionZoomFlipX();
            pScene.InitWithDuration(t, s, o);
            return pScene;
        }
    }
}