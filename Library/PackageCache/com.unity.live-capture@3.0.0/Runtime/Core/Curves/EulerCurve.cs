using System;
using UnityEngine;

namespace Unity.LiveCapture
{
    /// <summary>
    /// A type of <see cref="ICurve"/> that stores keyframes of type <see cref="Quaternion"/> as Euler angles.
    /// </summary>
    public class EulerCurve : ICurve<Quaternion>, IReduceableCurve
    {
        Vector3Sampler m_Sampler = new Vector3Sampler();
        Vector3TangentUpdater m_TangentUpdater = new Vector3TangentUpdater();
        EulerKeyframeReducer m_Reducer = new EulerKeyframeReducer();
        AnimationCurve[] m_Curves;
        Vector3? m_EulerAngles;

        /// <inheritdoc/>
        public float MaxError
        {
            get => m_Reducer.MaxError;
            set => m_Reducer.MaxError = value;
        }

        /// <inheritdoc/>
        public string RelativePath { get; }

        /// <inheritdoc/>
        public string PropertyName { get; }

        /// <inheritdoc/>
        public Type BindingType { get; }

        /// <inheritdoc/>
        public FrameRate FrameRate
        {
            get => m_Sampler.FrameRate;
            set => m_Sampler.FrameRate = value;
        }

        /// <summary>
        /// Creates a new <see cref="EulerCurve"/> instance.
        /// </summary>
        /// <param name="relativePath">The path of the game object this curve applies to,
        /// relative to the game object the actor component is attached to.</param>
        /// <param name="propertyName">The name or path to the property that is animated.</param>
        /// <param name="bindingType">The type of component this curve is applied to.</param>
        public EulerCurve(string relativePath, string propertyName, Type bindingType)
        {
            RelativePath = relativePath;
            PropertyName = propertyName;
            BindingType = bindingType;

            Reset();
        }

        /// <inheritdoc/>
        public void AddKey(double time, Quaternion value)
        {
            if (m_EulerAngles.HasValue)
                m_EulerAngles = MathUtility.ClosestEuler(value, m_EulerAngles.Value);
            else
                m_EulerAngles = value.eulerAngles;

            m_Sampler.Add((float)time, m_EulerAngles.Value);

            Sample();
        }

        /// <inheritdoc/>
        public bool IsEmpty()
        {
            return m_Sampler.IsEmpty()
                && m_TangentUpdater.IsEmpty()
                && m_Reducer.IsEmpty()
                && m_Curves[0].length == 0;
        }

        /// <inheritdoc/>
        public void Clear()
        {
            Reset();
        }

        /// <inheritdoc/>
        public void SetToAnimationClip(AnimationClip clip)
        {
            Flush();

            if (m_Curves[0].length > 0)
                clip.SetCurve(RelativePath, BindingType, $"{PropertyName}.x", m_Curves[0]);
            if (m_Curves[1].length > 0)
                clip.SetCurve(RelativePath, BindingType, $"{PropertyName}.y", m_Curves[1]);
            if (m_Curves[2].length > 0)
                clip.SetCurve(RelativePath, BindingType, $"{PropertyName}.z", m_Curves[2]);
        }

        void Reset()
        {
            m_EulerAngles = null;
            m_Sampler.Reset();
            m_TangentUpdater.Reset();
            m_Reducer.Reset();
            m_Curves = new[]
            {
                new AnimationCurve(),
                new AnimationCurve(),
                new AnimationCurve()
            };
        }

        void Flush()
        {
            m_Sampler.Flush();
            m_TangentUpdater.Flush();
            m_Reducer.Flush();

            Sample();
        }

        void Sample()
        {
            while (m_Sampler.MoveNext())
            {
                var sample = m_Sampler.Current;

                m_TangentUpdater.Add(new Keyframe<Vector3>()
                {
                    Time = sample.Time,
                    Value = sample.Value
                });
            }

            while (m_TangentUpdater.MoveNext())
            {
                m_Reducer.Add(m_TangentUpdater.Current);
            }

            while (m_Reducer.MoveNext())
            {
                AddKey(m_Reducer.Current);
            }
        }

        void AddKey(Keyframe<Vector3> keyframe)
        {
            for (var i = 0; i < 3; ++i)
            {
                m_Curves[i].AddKey(new Keyframe()
                {
                    time = keyframe.Time,
                    value = keyframe.Value[i],
                    inTangent = keyframe.InTangent[i],
                    outTangent = keyframe.OutTangent[i]
                });
            }
        }
    }
}
