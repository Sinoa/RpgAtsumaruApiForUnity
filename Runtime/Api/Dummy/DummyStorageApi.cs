// zlib/libpng License
//
// Copyright (c) 2018 Sinoa
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it freely,
// subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented; you must not claim that you wrote the original software.
//    If you use this software in a product, an acknowledgment in the product documentation would be appreciated but is not required.
// 2. Altered source versions must be plainly marked as such, and must not be misrepresented as being the original software.
// 3. This notice may not be removed or altered from any source distribution.

using System;
using System.Threading.Tasks;

namespace RpgAtsumaruApiForUnity
{
    internal sealed class DummyRpgAtsumaruStorage : RpgAtsumaruStorage
    {
        internal DummyRpgAtsumaruStorage(RpgAtsumaruApi.RpgAtsumaruApiCallbackReceiver receiver) : base(receiver)
        {
        }

        public override Task SyncSaveDataAsync()
        {
            return Task.CompletedTask;
        }

        public override void SetSystemData(string systemData)
        {
        }

        public override string GetSystemData()
        {
            return string.Empty;
        }

        public override bool ContainsSystemData()
        {
            return false;
        }

        public override void SetSaveData(int slotId, string saveData)
        {
        }

        public override string GetSaveData(int slotId)
        {
            return string.Empty;
        }

        public override bool ContainsSaveData(int slotId)
        {
            return false;
        }

        public override int[] GetAllSaveDataSlotId()
        {
            return Array.Empty<int>();
        }
    }
}