// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.CodeDom
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;


    /// <devdoc>
    ///     <para>
    ///       A collection that stores <see cref='Microsoft.CodeDom.CodeTypeReference'/> objects.
    ///    </para>
    /// </devdoc>
    [
        //  ClassInterface(ClassInterfaceType.AutoDispatch),
        ComVisible(true),
    // Serializable,
    ]
    public class CodeTypeReferenceCollection : CollectionBase
    {
        /// <devdoc>
        ///     <para>
        ///       Initializes a new instance of <see cref='Microsoft.CodeDom.CodeTypeReferenceCollection'/>.
        ///    </para>
        /// </devdoc>
        public CodeTypeReferenceCollection()
        {
        }

        /// <devdoc>
        ///     <para>
        ///       Initializes a new instance of <see cref='Microsoft.CodeDom.CodeTypeReferenceCollection'/> based on another <see cref='Microsoft.CodeDom.CodeTypeReferenceCollection'/>.
        ///    </para>
        /// </devdoc>
        public CodeTypeReferenceCollection(CodeTypeReferenceCollection value)
        {
            this.AddRange(value);
        }

        /// <devdoc>
        ///     <para>
        ///       Initializes a new instance of <see cref='Microsoft.CodeDom.CodeTypeReferenceCollection'/> containing any array of <see cref='Microsoft.CodeDom.CodeTypeReference'/> objects.
        ///    </para>
        /// </devdoc>
        public CodeTypeReferenceCollection(CodeTypeReference[] value)
        {
            this.AddRange(value);
        }

        /// <devdoc>
        /// <para>Represents the entry at the specified index of the <see cref='Microsoft.CodeDom.CodeTypeReference'/>.</para>
        /// </devdoc>
        public CodeTypeReference this[int index]
        {
            get
            {
                return ((CodeTypeReference)(List[index]));
            }
            set
            {
                List[index] = value;
            }
        }

        /// <devdoc>
        ///    <para>Adds a <see cref='Microsoft.CodeDom.CodeTypeReference'/> with the specified value to the 
        ///    <see cref='Microsoft.CodeDom.CodeTypeReferenceCollection'/> .</para>
        /// </devdoc>
        public int Add(CodeTypeReference value)
        {
            return List.Add(value);
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public void Add(string value)
        {
            Add(new CodeTypeReference(value));
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public void Add(Type value)
        {
            Add(new CodeTypeReference(value));
        }

        /// <devdoc>
        /// <para>Copies the elements of an array to the end of the <see cref='Microsoft.CodeDom.CodeTypeReferenceCollection'/>.</para>
        /// </devdoc>
        public void AddRange(CodeTypeReference[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            for (int i = 0; ((i) < (value.Length)); i = ((i) + (1)))
            {
                this.Add(value[i]);
            }
        }

        /// <devdoc>
        ///     <para>
        ///       Adds the contents of another <see cref='Microsoft.CodeDom.CodeTypeReferenceCollection'/> to the end of the collection.
        ///    </para>
        /// </devdoc>
        public void AddRange(CodeTypeReferenceCollection value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            int currentCount = value.Count;
            for (int i = 0; i < currentCount; i = ((i) + (1)))
            {
                this.Add(value[i]);
            }
        }

        /// <devdoc>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='Microsoft.CodeDom.CodeTypeReferenceCollection'/> contains the specified <see cref='Microsoft.CodeDom.CodeTypeReference'/>.</para>
        /// </devdoc>
        public bool Contains(CodeTypeReference value)
        {
            return List.Contains(value);
        }

        /// <devdoc>
        /// <para>Copies the <see cref='Microsoft.CodeDom.CodeTypeReferenceCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </devdoc>
        public void CopyTo(CodeTypeReference[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <devdoc>
        ///    <para>Returns the index of a <see cref='Microsoft.CodeDom.CodeTypeReference'/> in 
        ///       the <see cref='Microsoft.CodeDom.CodeTypeReferenceCollection'/> .</para>
        /// </devdoc>
        public int IndexOf(CodeTypeReference value)
        {
            return List.IndexOf(value);
        }

        /// <devdoc>
        /// <para>Inserts a <see cref='Microsoft.CodeDom.CodeTypeReference'/> into the <see cref='Microsoft.CodeDom.CodeTypeReferenceCollection'/> at the specified index.</para>
        /// </devdoc>
        public void Insert(int index, CodeTypeReference value)
        {
            List.Insert(index, value);
        }

        /// <devdoc>
        ///    <para> Removes a specific <see cref='Microsoft.CodeDom.CodeTypeReference'/> from the 
        ///    <see cref='Microsoft.CodeDom.CodeTypeReferenceCollection'/> .</para>
        /// </devdoc>
        public void Remove(CodeTypeReference value)
        {
            List.Remove(value);
        }
    }
}
