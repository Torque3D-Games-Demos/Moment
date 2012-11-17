//-----------------------------------------------------------------------------
// Copyright (c) 2012 Daniel Buckmaster
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------

$AIPlayer::SpeechSlot = 0;

function AIPlayer::say(%this, %phrase, %voice)
{
   if (!isObject(%voice))
      %voice = DefaultVoice;
   
   %this.stopTalking();
   
   %sound = %voice.getValue(%voice.getIndexFromKey(%phrase));
   if (isObject(%sound))
   {
      %this.playAudio($AIPlayer::SpeechSlot, %sound);
      %this.stopTalking = %this.getDataBlock().schedule(%sound.getSoundDuration() * 1000, "onFinishedTalking", %this);
   }
}

function AIPlayer::stopTalking(%this)
{
   %this.stopAudio($AIPlayer::SpeechSlot);
   if (%this.stopTalking)
   {
      cancel(%this.stopTalking);
      %this.getDatablock().onFinishedTalking(%this);
   }
}

function PlayerData::onFinishedTalking(%this, %obj)
{
   %obj.stopTalking = 0;
}

new ArrayObject(DefaultVoice);
DefaultVoice.add("ouch", HumanMalePainSound);
DefaultVoice.add("ouch!", HumanMaleDeathSound);
