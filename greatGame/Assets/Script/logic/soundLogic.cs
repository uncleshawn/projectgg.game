using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class soundLogic {
		
		private static soundLogic mInstance;

                //public bool sound;
                //public bool effect;
                private float mEffectVal;
                private float mBkVal;

                public float EffectVal { get { return mEffectVal; } set { 
                        mEffectVal = value;
                        foreach (AudioSource src in mAudioSrcs) {
                                src.volume = mEffectVal;
                        }
                } }
                public float BkVal { get { return mBkVal; } set { 
                        mBkVal = value;
                        mBkAudioSrc.volume = value;
                } }
                
                private GameObject mAudioObj;

                private List<AudioSource> mAudioSrcs;
                private AudioSource mBkAudioSrc;

		private soundLogic(){
                        string audioPrefab = "Prefabs/ui/audio";
                        mAudioObj = (GameObject)GameObject.Instantiate(Resources.Load(audioPrefab), new Vector3(0, 0, 0), Quaternion.identity);
                        GameObject.DontDestroyOnLoad(mAudioObj);

                        mAudioSrcs = new List<AudioSource>();
                        mBkAudioSrc = mAudioObj.AddComponent("AudioSource") as AudioSource;

                        mEffectVal = 0.1f;
                        mBkVal = 0.05f;
		}

                private AudioSource getUnUseAudioSrc(){
                        foreach(AudioSource src in mAudioSrcs){
                                if(!src.isPlaying){
                                        return src;
                                }
                        }
                        //mAudioObj.AddComponent<AudioSource>(audioSrc);
                        AudioSource audioSrc = mAudioObj.AddComponent("AudioSource") as AudioSource;
                        mAudioSrcs.Add(audioSrc);
                        return audioSrc;
                }

		static public soundLogic getInstance(){
				if (mInstance == null) {
						mInstance = new soundLogic();
				}
				return mInstance;
		}

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		public void playEffect(string name){
                        if (!name.StartsWith("audio/")) {
                                name = "audio/" + name;
                        }
                        AudioSource src = getUnUseAudioSrc();
                        src.clip = (AudioClip)Resources.Load(name, typeof(AudioClip));//调用Resources方法加载AudioClip资源
                        src.volume = mEffectVal;
                        src.Play();
		}

                public void playBackGround(string name) {
                        if (!name.StartsWith("audio/")) {
                                name = "audio/" + name;
                        }
                        mBkAudioSrc.clip = (AudioClip)Resources.Load(name, typeof(AudioClip));//调用Resources方法加载AudioClip资源
                        mBkAudioSrc.loop = true;
                        mBkAudioSrc.volume = mBkVal;
                        mBkAudioSrc.Play();	
		}

}
