using System;
using System.Collections.Generic;
using UnityEngine;

namespace CommonUtility.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] Rescale<T>(this T[] array, int length)
        {
            if (array == null)
                return null;

            if (array.Length == length)
                return array;

            T[] result = new T[length];
            int min = Mathf.Min(length, array.Length);
            for (int i = 0; i < min; i++)
                result[i] = array[i];

            return result;
        }

        public static void Fill<T>(this T[] array, IReadOnlyList<T> source)
        {
            if (array.Length != source.Count)
                throw new InvalidOperationException("Different array sizes");

            for (int i = 0; i < array.Length; i++)
                array[i] = source[i];
        }
    }

    public static class AnimationExtentions
    {
        private const float _defaultSpeed = 1f;

        public static void ToStartClip(this Animation animation, AnimationClip animationClip) =>
            SetClipToTime(animation, animationClip, 0);

        public static void FinishClip(this Animation animation, AnimationClip animationClip) =>
            SetClipToTime(animation, animationClip, animationClip.length);

        public static void SetClip(this Animation animation, AnimationClip animationClip, float blend = 0f)
        {
            AnimationClip clip = animation.GetClip(animationClip.name);

            if (clip != animationClip)
            {
                if (clip != null)
                    animation.RemoveClip(animationClip.name);

                animation.AddClip(animationClip, animationClip.name);
            }

            animation[animationClip.name].speed = _defaultSpeed;
            animation.Stop();

            if (blend > float.Epsilon)
                animation.Blend(animationClip.name, blend);
            else
                animation.Play(animationClip.name);
        }

        private static void SetClipToTime(this Animation animation, AnimationClip animationClip, float time)
        {
            animation.SetClip(animationClip, 0);
            animation[animationClip.name].time = time;
            animation[animationClip.name].speed = 0f;
        }
    }
}