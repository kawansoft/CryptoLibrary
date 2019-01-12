﻿/* 
 * This file is part of Safester C# OpenPGP SDK.                                
 * Copyright(C) 2019,  KawanSoft SAS
 * (http://www.kawansoft.com). All rights reserved.                                
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License. 
 */
using Safester.CryptoLibrary.Api;
using System;

namespace CryptoLibrary.Samples.Src
{
    public class PgpKeyPairGeneratorSample
    {

        private PgpPairKeyring pgpPairKeyring = null;

        public PgpPairKeyring PgpPairKeyring { get => pgpPairKeyring; }

        public void Generate(string identity, string passphrase, PgpAsymAlgo pgpAsymAlgo, PgpAsymKeyLength pgpAsymKeyLength)
        {
            Console.WriteLine(DateTime.Now + " Starting generation...");
            PgpKeyPairGenerator pgpKeyPairGenerator = new PgpKeyPairGenerator(identity, passphrase.ToCharArray(), pgpAsymAlgo, pgpAsymKeyLength);
            pgpPairKeyring = pgpKeyPairGenerator.Generate();

            Console.WriteLine(DateTime.Now + " Done");
            Console.WriteLine();
            Console.WriteLine(PgpPairKeyring.PrivateKeyRing);
            Console.WriteLine();
            Console.WriteLine(PgpPairKeyring.PublicKeyRing);
            Console.WriteLine();
            Console.WriteLine("Press enter to close....");
            Console.ReadLine();
      
        }



    }
}