﻿/* 
 * This file is part of Safester C# OpenPGP SDK.                                
 * Copyright(C) 2019,  KawanSoft SAS
 * (https://www.safester.net). All rights reserved.                                
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

using Org.BouncyCastle.Bcpg.OpenPgp;
using Safester.CryptoLibrary.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safester.CryptoLibrary.Api
{
    /// <summary>
    /// Allows to verity if a PgpPublic key is signed by a PGP Master key 
    /// </summary>
    public class PgpPublicKeyVerifier
    {
        /// <summary>
        /// The Exception thrown by PGP public key verification, if any Defaults to null if no Exception is thrown.
        /// </summary>
        private Exception exception = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PgpPublicKeyVerifier()
        {

        }

        /// <summary>
        /// Returns the Exception thrown by PGP public key verification, if any. Defaults to null if no Exception is thrown.
        /// </summary>
        /// <returns></returns>
        public Exception GetException()
        {
            return exception;
        }

        /// <summary>
        /// Verify that the passed PGP Public key is signed by the passed PGP master signature key.
        /// </summary>
        /// <param name="publicKeyRing">The encryption  PGP public keyring to test the signature from</param>
        /// <param name="publicKeyRingMaster">The signature PGP public key to use to check the signature</param>
        /// <returns>true if the if the sigature is valid, else false</returns>
        public bool VerifySignature(string publicKeyRing, string publicKeyRingMaster)
        {
            PgpPublicKey pgpPublicKey = PgpPublicKeyGetter.ReadPublicKey(publicKeyRing);
            PgpPublicKey pgpPublicKeyMaster = PgpSignPublicKeyGetter.ReadPublicKey(publicKeyRingMaster);
            bool verify = VerifySignature(pgpPublicKey, publicKeyRingMaster);
            return verify;
        }

        /// <summary>
        /// Verify that the passed PGP Public key is signed by the passed PGP master signature key.
        /// </summary>
        /// <param name="pgpPublicKey">The encryption PGP public to test the signature from</param>
        /// <param name="publicKeyRingMaster">The signature PGP public key to use to check the signature</param>
        /// <returns>true if the if the sigature is valid, else false</returns>
        public bool VerifySignature(PgpPublicKey pgpPublicKey, string publicKeyRingMaster)
        {
            this.exception = null;

            PgpPublicKey pgpPublicKeyMaster = PgpSignPublicKeyGetter.ReadPublicKey(publicKeyRingMaster);

            foreach (PgpSignature sig in pgpPublicKey.GetKeySignatures())
            {
                if (sig.KeyId != pgpPublicKeyMaster.KeyId)
                {
                    continue;
                }

                try
                {
                    sig.InitVerify(pgpPublicKeyMaster);

                    if (sig.SignatureType == PgpSignature.SubkeyBinding)
                    {
                        bool verify = sig.VerifyCertification(pgpPublicKeyMaster, pgpPublicKey);
                        return verify;
                    }

                }
                catch (Exception exception)
                {
                    this.exception = exception;
                }
            }

            return false;
        }
    }
}
